import React, { useCallback, useContext } from "react";
import { useRouter } from "../../utils/useRouter";
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
import File from "../File";
import { Meta } from "../../Meta";
import { DirectoryContainer, DirectoryListing, DirectoryEntry, DirectoryTitle } from "../Basics";

const Directory = ({
  name,
  urlPath,
  files,
  directories
}: Meta) => {
  const router = useRouter();
  const size = useContext(ThumbnailSizeContext);
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
    [urlPath, api]
  );
  return (
    <FileDrop onDrop={handleFileDrop}>
      <DirectoryContainer>
        <DirectoryTitle onClick={() => router.push(urlPath)}>{name}</DirectoryTitle>
        {directories && <DirectoryListing>
          {Object.entries(directories).map(([k, v]) => (
            <DirectoryEntry key={k}><LayoutSelector {...v} key={k} /></DirectoryEntry>
          ))}

        </DirectoryListing>}
        {files && <DirectoryListing direction='row'>
          {Object.entries(files).map(([k, v]) => (
            <FileDrag relativePath={v.urlPath} name={k} key={k}>
              <DirectoryEntry key={k}>
                <File name={k} urlPath={v.urlPath} properties={v} width={100} height={150} />
              </DirectoryEntry>
            </FileDrag>
          ))}
        </DirectoryListing>}
      </DirectoryContainer></FileDrop>
  );
};

export default Directory;
