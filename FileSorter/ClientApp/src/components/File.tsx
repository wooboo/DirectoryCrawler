import React, { useContext, forwardRef } from 'react';
import { Properties } from '../Meta';
import Thumbnail from './Thumbnail';
import styled from 'styled-components';
import StyledFileToolbar from './FileToolbar';
import SettingsContext from '../SettingsContext';

interface Props {
  name: string;
  urlPath: string;
  properties?: Properties;
  width: number;
  height: number;
  isDragging?: boolean;
}
const Container = styled.div<{
  width: number;
  height: number;
  isDragging?: boolean;
}>`
  position: relative;
  margin: 2px;
  padding: 2px;
  border-width: 1px;
  border-style: solid;
  border-color: gray;
  width: ${({ width }) => width}px;
  height: ${({ height }) => height}px;
  > .file-toolbar {
    display: none;
  }
  :hover > .file-toolbar {
    display: block;
  }
  overflow-y: auto;
  flex: 1;
`;
const Caption = styled.span`
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(197, 197, 197, 0.86);
  border-radius: 4px;
  padding: 4px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
`;

export default forwardRef<HTMLDivElement, Props>(function File(
  { name, urlPath, width, height, isDragging }: Props,
  ref,
) {
  const { filePreviewApi } = useContext(SettingsContext);
  return (
    <Container ref={ref} width={width} height={height} isDragging={isDragging}>
      <StyledFileToolbar urlPath={urlPath} onZoom={() => {}} />
      <Thumbnail src={`${filePreviewApi}${urlPath}`} height={height} />
      <Caption title={name}>{name}</Caption>
    </Container>
  );
});
