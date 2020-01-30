import React, { useCallback, useContext } from "react";
import { useRouter } from "../../utils/useRouter";
import FileDrop from "../FileDrop";
import FileDrag from "../FileDrag";
import LayoutSelector from "../LayoutSelector";
import File from "../File";
import { Meta } from "../../Meta";
import { DirectoryContainer, DirectoryListing, DirectoryEntry, DirectoryTitle } from "../Basics";
import DirectoryEntries from "../DirectoryEntries";

const Directory = ({
  name,
  urlPath,
  files,
  directories
}: Meta) => {
  const router = useRouter();

  return (
    <FileDrop urlPath={urlPath}>
      <DirectoryContainer>
        <DirectoryTitle onClick={() => router.push(urlPath)}>{name}</DirectoryTitle>
        <DirectoryEntries files={files} directories={directories}/>
      </DirectoryContainer></FileDrop>
  );
};

export default Directory;
