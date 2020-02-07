import { ReactElement, useCallback, useContext } from 'react';
import { NativeTypes } from 'react-dnd-html5-backend';
import { useDrop, DragElementWrapper, DragSourceOptions } from 'react-dnd';
import ApiContext from '../ApiContext';

interface Props {
  urlPath: string;
  name: string;
  children: (drag: DragElementWrapper<DragSourceOptions>, isActive: boolean) => ReactElement;
}

function FileDrop(props: Props): ReactElement {
  const { children, urlPath } = props;
  const { reloadAll, uploadFiles, moveFiles } = useContext(ApiContext);
  const handleFileDrop = useCallback(
    async (item, monitor) => {
      if (monitor) {
        const monitorItem = monitor.getItem();
        const itemType = monitor.getItemType();
        if (itemType === NativeTypes.FILE) {
          const files = monitorItem.files;
          await uploadFiles(urlPath, files);
          reloadAll();
        } else if (itemType === 'file') {
          if (urlPath !== monitorItem.urlPath) {
            await moveFiles(urlPath, [monitorItem.urlPath]);
            await new Promise(resolve => setTimeout(resolve, 200));
            reloadAll();
          }
        }
      }
    },
    [urlPath, uploadFiles, moveFiles, reloadAll],
  );
  const [{ canDrop, isOverCurrent }, drop] = useDrop({
    accept: [NativeTypes.FILE, 'file'],
    drop(item, monitor) {
      const didDrop = monitor.didDrop();
      if (didDrop) {
        return;
      }
      handleFileDrop(item, monitor);
    },
    collect: monitor => ({
      isOver: monitor.isOver(),
      isOverCurrent: monitor.isOver({ shallow: true }),
      canDrop: monitor.canDrop(),
    }),
  });
  const isActive = canDrop && isOverCurrent;

  return children(drop, isActive);
}

export default FileDrop;
