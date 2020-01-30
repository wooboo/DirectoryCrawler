import React, { useCallback, useContext } from "react";
import { useRouter } from "../../utils/useRouter";
import { trigger } from "swr";
import FileDrop from "../FileDrop";
import FileDrag from "../FileDrag";
import Thumbnail from "../Thumbnail";
import ThumbnailSizeContext from "../../ThumbnailSizeContext";
import { NativeTypes } from "react-dnd-html5-backend";
import uploadFiles from "../../utils/uploadFiles";
import pusher from "../../utils/pusher";
import LayoutSelector from "../LayoutSelector";
import { Meta } from "../../Meta";
import { DirectoryContainer, DirectoryListing, DirectoryEntry, DirectoryTitle } from "../Basics";
import File from "../File";



const Temp = ({
  name,
  urlPath,
  files,
  directories
}: Meta) => {
  const size = useContext(ThumbnailSizeContext);
  return (
    <DirectoryContainer>
      <DirectoryTitle>{name}</DirectoryTitle>
      <DirectoryListing>
        {directories &&
          Object.entries(directories).map(([k, v]) => (
            <DirectoryEntry key={k}><LayoutSelector {...v} key={k} /></DirectoryEntry>
          ))}
      </DirectoryListing>
      <DirectoryListing direction='row'>
        {files &&
          Object.entries(files).map(([k, v]) => (
            <FileDrag relativePath={v.urlPath} name={k} key={k}>
              <DirectoryEntry key={k}>
                <File name={k} urlPath={v.urlPath} properties={v} width={100} height={150} />
              </DirectoryEntry>
            </FileDrag>
          ))}
      </DirectoryListing>
    </DirectoryContainer>
  );
};

export default Temp;
