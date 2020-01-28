import React from "react";
import { NativeTypes } from "react-dnd-html5-backend";
import { useDrop, DragObjectWithType, DropTargetMonitor } from "react-dnd";

function FileDrop(props: {
  onDrop: (item: DragObjectWithType, monitor: DropTargetMonitor) => void;
  children: any;
}) {
  const { onDrop, children } = props;
  const [{ canDrop, isOver, isOverCurrent }, drop] = useDrop({
    accept: [NativeTypes.FILE, "file"],
    drop(item, monitor) {
      const didDrop = monitor.didDrop()
      if (didDrop) {
        return
      }
      if (onDrop) {
        onDrop(item, monitor);
      }
    },
    collect: monitor => ({
      isOver: monitor.isOver(),
      isOverCurrent: monitor.isOver({ shallow: true }),
      canDrop: monitor.canDrop()
    })
  });
  const isActive = canDrop && isOverCurrent;

  return (
    <div ref={drop} style={{ opacity: isActive ? 0.2 : 1 }}>
      {children}
    </div>
  );
}

export default FileDrop;
