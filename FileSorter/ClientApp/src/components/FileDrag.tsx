import React from "react";
import { useDrag } from "react-dnd";

function FileDrag({
  urlPath,
  name,
  children
}: {
  urlPath: string;
  name: string;
  children: any;
}) {
  const [{ isDragging }, drag] = useDrag({
    item: { urlPath, name, type: "file" },
    end: (item, monitor) => {
      const dropResult = monitor.getDropResult();
      if (item && dropResult) {
        console.log(`You dropped`, item, `into`, dropResult, monitor);
      }
    },
    collect: monitor => ({
      isDragging: monitor.isDragging()
    })
  });

  return (
    <div ref={drag} style={{ opacity: isDragging ? 0.2 : 1 }}>
      {children}
    </div>
  );
}

export default FileDrag;
