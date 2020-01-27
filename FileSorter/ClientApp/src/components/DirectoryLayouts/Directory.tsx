import React, { useCallback, useContext } from "react";
import { trigger } from "swr";
import styled from "styled-components";
import FileDrop from "../FileDrop";
import FileDrag from "../FileDrag";
import Thumbnail from "../Thumbnail";
import ThumbnailSizeContext from "../../ThumbnailSizeContext";
import { NativeTypes } from "react-dnd-html5-backend";
import uploadFiles from "../../utils/uploadFiles";
import pusher from "../../utils/pusher";
import LayoutSelector from "../LayoutSelector";
import { Meta } from "../../Meta";

const DirectoryEntry = styled.li`
  list-style: none;
  padding: 4px;
  box-shadow: 2px;
`;
const Directory = ({
  name,
  urlPath,
  files,
  directories
}:Meta) => {
  const size = useContext(ThumbnailSizeContext);
  return (
    <>
      {name}
      <ul>
        {files &&
          Object.entries(files).map(([k, v]) => (
            <DirectoryEntry key={k}>
              <Thumbnail src={`files/${v.urlPath}`} width={size} />
              {k}
            </DirectoryEntry>
          ))}
        {directories &&
          Object.entries(directories).map(([k, v]) => (
            <DirectoryEntry key={k}><LayoutSelector {...v} key={k} /></DirectoryEntry>
          ))}
      </ul>
    </>
  );
};

export default Directory;
