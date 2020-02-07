import React, { ReactElement } from 'react';
import styled from 'styled-components';

interface Props {
  urlPath: string;
  onZoom: () => void;
  className?: string;
}

function FileToolbar({ className, urlPath, onZoom }: Props): ReactElement {
  return (
    <span className={`file-toolbar ${className}`}>
      <span onClick={onZoom}>+</span>
    </span>
  );
}
const StyledFileToolbar = styled(FileToolbar)`
  position: absolute;
  top: 2px;
  left: 2px;
  right: 2px;
  background-color: rgba(197, 197, 197, 0.86);
  border-radius: 4px;
  padding: 4px;
  overflow: hidden;
`;

export default StyledFileToolbar;
