import { useDrag, DragElementWrapper, DragSourceOptions } from 'react-dnd';
import { ReactElement } from 'react';

interface Props {
  urlPath: string;
  name: string;
  children: (drag: DragElementWrapper<DragSourceOptions>, isDragging: boolean) => ReactElement;
}

export default function FileDrag({ urlPath, name, children }: Props): ReactElement {
  const [{ isDragging }, drag] = useDrag({
    item: { urlPath, name, type: 'file' },
    end: (item, monitor) => {
      const dropResult = monitor.getDropResult();
      if (item && dropResult) {
        console.log(`You dropped`, item, `into`, dropResult, monitor);
      }
    },
    collect: monitor => ({
      isDragging: monitor.isDragging(),
    }),
  });

  return children(drag, isDragging);
}
