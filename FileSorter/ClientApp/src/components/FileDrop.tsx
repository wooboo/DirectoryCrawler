import { ReactElement, useCallback } from 'react';
import { NativeTypes } from 'react-dnd-html5-backend';
import { useDrop, DragElementWrapper, DragSourceOptions } from 'react-dnd';
import { trigger } from 'swr';
import pusher from '../utils/pusher';
import uploadFiles from '../utils/uploadFiles';
import { useRouter } from '../utils/useRouter';

interface Props {
  urlPath: string;
  name: string;
  children: (drag: DragElementWrapper<DragSourceOptions>, isActive: boolean) => ReactElement;
}

function FileDrop(props: Props): ReactElement {
  const router = useRouter();
  const { children, urlPath } = props;
  const api = `/files${urlPath}`;
  const handleFileDrop = useCallback(
    async (item, monitor) => {
      if (monitor) {
        const monitorItem = monitor.getItem();
        const itemType = monitor.getItemType();
        if (itemType === NativeTypes.FILE) {
          const files = monitorItem.files;
          await uploadFiles(api, files);
          trigger(`/directories${router.pathname}`);
        } else if (itemType === 'file') {
          if (urlPath !== monitorItem.urlPath) {
            await pusher(api, 'PUT', [monitorItem.urlPath]);
            await new Promise(resolve => setTimeout(resolve, 200));
            trigger(`/directories${router.pathname}`);
          }
        }
      }
    },
    [urlPath, api],
  );
  const [{ canDrop, isOver, isOverCurrent }, drop] = useDrop({
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
