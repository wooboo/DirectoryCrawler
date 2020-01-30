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
import { Meta } from "../../Meta";
import { DirectoryContainer, DirectoryListing, DirectoryEntry, DirectoryTitle } from "../Basics";

const RightContainer = styled.div`
  overflow-y: auto;
  grid-area: content;
`;
const LeftContainer = styled.div`
  overflow-y: auto;
  grid-area: tmp;
`;

const Container = styled.div`
  display: grid;
  grid-template-columns: 35% auto;
  grid-template-rows: 0fr auto;
  grid-template-areas: 
    "header header"
    "tmp content";
  height: 100%;
`;
const MonthFull = ({
  name,
  urlPath,
  files,
  directories
}: Meta) => {
  const router = useRouter();
  const tmp = directories && directories["TMP"];
  const notTmp =
    directories && Object.values(directories).filter(o => o.name !== "TMP");
  return (
    <Container>

      <DirectoryTitle onClick={() => router.push(urlPath)}>{urlPath}</DirectoryTitle>
      <LeftContainer>
        {tmp && <DirectoryEntry key={'TMP'}><LayoutSelector {...tmp} key={'TMP'} /></DirectoryEntry>}
      </LeftContainer>
      <RightContainer>
        {notTmp && <DirectoryListing direction="column">
          {Object.entries(notTmp).map(([k, v]) => (
            <DirectoryEntry key={k}><LayoutSelector {...v} key={k} /></DirectoryEntry>
          ))}
        </DirectoryListing>}
        {files && <DirectoryListing direction="row">
          {Object.entries(files).map(([k, v]) => (
            <DirectoryEntry key={k}>
              <Thumbnail src={`files/${v.urlPath}`}  width={100} height={150}  />
              {k}
            </DirectoryEntry>
          ))}
        </DirectoryListing>}
      </RightContainer>
    </Container>
  );
};

export default MonthFull;
