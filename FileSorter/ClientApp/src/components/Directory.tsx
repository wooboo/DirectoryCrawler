import React, { useCallback, useContext } from "react";
import { trigger } from "swr";
import styled from "styled-components";
import FileDrop from "./FileDrop";
import FileDrag from "./FileDrag";
import Thumbnail from "./Thumbnail";
import ThumbnailSizeContext from "../ThumbnailSizeContext";
import { NativeTypes } from "react-dnd-html5-backend";
import uploadFiles from "../utils/uploadFiles";
import pusher from "../utils/pusher";

const File = styled.li`
  list-style: none;
  padding: 4px;
  box-shadow: 2px;
`;
const Directory = ({
  name,
  urlPath,
  files,
  directories
}: {
  name: string;
  urlPath: string;
  files: any;
  directories: any;
}) => {
  const api = `/files/${urlPath}`;
  const handleFileDrop = useCallback(
    async (item, monitor) => {
      if (monitor) {
        const monitorItem = monitor.getItem();
        const itemType = monitor.getItemType();
        if (itemType === NativeTypes.FILE) {
          const files = monitorItem.files;
          await uploadFiles(api, files);
          trigger(`/directories/`);
        } else if (itemType === "file") {
          if (urlPath !== monitorItem.urlPath) {
            await pusher(api, "PUT", [monitorItem.urlPath]);
            await new Promise(resolve => setTimeout(resolve, 200));
            trigger(`/directories/`);
          }
        }
      }
    },
    [relativePath, api]
  );
  const size = useContext(ThumbnailSizeContext);

  return (
    <FileDrop onDrop={handleFileDrop}>
      {name}
      <ul>
        {files &&
          Object.entries(files).map(([k, v]) => (
            <FileDrag relativePath={v.relativePath} name={k} key={k}>
              <File>
                <Thumbnail src={`files/${v.relativePath}`} width={size} />
                {k}
              </File>
            </FileDrag>
          ))}
        {directories &&
          Object.entries(directories).map(([k, v]) => (
            <FileDrag relativePath={v.relativePath} name={k} key={k}>
              <Directory
                name={k}
                relativePath={v.relativePath}
                files={v.files}
                directories={v.directories}
              />
            </FileDrag>
          ))}
      </ul>
    </FileDrop>
  );
};

export default Directory;
