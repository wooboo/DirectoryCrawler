import React, { useCallback, useContext } from "react";
import FileDrop from "../FileDrop";
import FileDrag from "../FileDrag";
import ThumbnailSizeContext from "../../ThumbnailSizeContext";
import LayoutSelector from "../LayoutSelector";
import { Meta } from "../../Meta";
import { DirectoryContainer, DirectoryListing, DirectoryEntry, DirectoryTitle } from "../Basics";
import File from "../File";
import DirectoryEntries from "../DirectoryEntries";



const Temp = ({
  name,
  urlPath,
  files,
  directories
}: Meta) => {
  const size = useContext(ThumbnailSizeContext);
  return (
    <FileDrop urlPath={urlPath}>
      <DirectoryContainer>
        <DirectoryTitle>{name}</DirectoryTitle>
        <DirectoryEntries files={files} directories={directories}/>
      </DirectoryContainer>
    </FileDrop>
  );
};

export default Temp;
