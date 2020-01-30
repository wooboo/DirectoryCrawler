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
import { Meta, Properties } from "../../Meta";
import { DirectoryContainer, DirectoryListing, DirectoryEntry, DirectoryTitle } from "../Basics";
import DirectoryEntries from "../DirectoryEntries";

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
  const notTmp = Object.keys(directories).reduce((object: {
    [name: string]: Meta<Properties>;
  }, key) => {
    if (key !== "TMP") {
      object[key] = directories[key]
    }
    return object
  }, {});
  return (
    <Container>
      <DirectoryTitle onClick={() => router.push(urlPath)}>{urlPath}</DirectoryTitle>
      <LeftContainer>
        {tmp && <DirectoryEntry key={'TMP'}><LayoutSelector {...tmp} key={'TMP'} /></DirectoryEntry>}
      </LeftContainer>
      <RightContainer>
        <DirectoryEntries files={files} directories={notTmp} />
      </RightContainer>
    </Container>
  );
};

export default MonthFull;
