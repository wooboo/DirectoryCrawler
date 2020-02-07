import React from 'react';
import FileDrop from '../FileDrop';
import { Meta } from '../../Meta';
import { DirectoryContainer } from '../Basics';
import DirectoryTitle from '../DirectoryTitle';
import DirectoryEntries from '../DirectoryEntries';

const Directory = ({ name, urlPath, files, directories }: Meta) => {
  return (
    <FileDrop urlPath={urlPath} name={name}>
      {(drag, isActive) => (
        <DirectoryContainer ref={drag} isDropping={isActive}>
          <DirectoryTitle urlPath={urlPath}>{name}</DirectoryTitle>
          <DirectoryEntries files={files} directories={directories} />
        </DirectoryContainer>
      )}
    </FileDrop>
  );
};

export default Directory;
