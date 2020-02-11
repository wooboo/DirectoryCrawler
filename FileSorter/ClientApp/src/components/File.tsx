import React, { useContext, useState, forwardRef, useRef } from 'react';
import { Properties } from '../Meta';
import Thumbnail from './Thumbnail';
import useComponentSize from '@rehooks/component-size';
import styled from 'styled-components';
import StyledFileToolbar from './FileToolbar';
import SettingsContext from '../SettingsContext';

interface Props {
  name: string;
  urlPath: string;
  properties?: Properties;
  isDragging?: boolean;
}
const Container = styled.div<{
  w: number;
  h: number;
  isDragging?: boolean;
  isSelected?: boolean;
}>`
  position: relative;
  margin: 2px;
  padding: 2px;
  border-width: 1px;
  border-style: solid;
  border-color: ${({ isDragging }) => (isDragging ? 'black' : 'gray')};
  min-width: ${({ w }) => w}px;
  width: ${({ isSelected }) => (isSelected ? '100%' : '1%')};
  ${({ isSelected, h }) =>
    !isSelected &&
    `
    height: ${h}px;
  `}
  > .file-toolbar {
    display: none;
  }
  :hover > .file-toolbar {
    display: block;
  }
  overflow: hidden;
`;
const Caption = styled.span`
  position: absolute;
  bottom: 2px;
  left: 2px;
  right: 2px;
  background-color: rgba(197, 197, 197, 0.86);
  border-radius: 4px;
  padding: 4px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
`;

export default forwardRef<HTMLDivElement, Props>(function File({ name, urlPath, isDragging }: Props, ref) {
  const { filePreviewApi, thumbnail } = useContext(SettingsContext);
  const [isSelected, setIsSelected] = useState(false);
  const r = useRef<HTMLDivElement>(null);
  const size = useComponentSize(r);
  return (
    <Container
      ref={x => {
        ((ref as unknown) as (f: HTMLDivElement | null) => void)(x);
        (r as { current: HTMLDivElement | null }).current = x;
      }}
      w={thumbnail.width}
      h={thumbnail.height}
      isSelected={isSelected}
      isDragging={isDragging}
    >
      <StyledFileToolbar urlPath={urlPath} onZoom={() => setIsSelected(!isSelected)} />
      <Thumbnail src={`${filePreviewApi}${urlPath}`} width={size.width} />
      <Caption title={name}>{name}</Caption>
    </Container>
  );
});
