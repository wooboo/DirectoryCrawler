import React from 'react';
import FileDrop from '../FileDrop';
import { Meta } from '../../Meta';
import { DirectoryContainer } from '../Basics';
import DirectoryTitle from '../DirectoryTitle';
import DirectoryEntries from '../DirectoryEntries';

const Temp = ({ name, urlPath, files, directories }: Meta) => {
  return (
    <FileDrop urlPath={urlPath} name={name}>
      {(drag)=>(
      <DirectoryContainer ref={drag}>
        <DirectoryTitle urlPath={urlPath}>{name}</DirectoryTitle>
        <DirectoryEntries files={files} directories={directories} />
      </DirectoryContainer>
      )}
    </FileDrop>
  );
};

export default Temp;
