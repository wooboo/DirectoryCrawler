import React from "react";
import PropTypes from "prop-types";
import { useDrag } from "react-dnd";

function FileDrag({
  relativePath,
  name,
  children
}: {
  relativePath: string;
  name: string;
  children: any;
}) {
  const [{ isDragging }, drag] = useDrag({
    item: { relativePath, name, type: "file" },
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

FileDrag.propTypes = {
  onDrop: PropTypes.func
};

export default FileDrag;
