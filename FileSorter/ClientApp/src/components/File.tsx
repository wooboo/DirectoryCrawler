import React, { useContext, useState, forwardRef, useRef } from 'react';
import { Properties } from '../Meta';
import Thumbnail from './Thumbnail';
import useComponentSize from '@rehooks/component-size';
import StyledFileToolbar from './FileToolbar';
import SettingsContext from '../SettingsContext';
import styles from './File.module.scss';

interface Props {
  name: string;
  urlPath: string;
  properties?: Properties;
  isDragging?: boolean;
}

export default forwardRef<HTMLDivElement, Props>(function File({ name, urlPath, isDragging }: Props, ref) {
  const { filePreviewApi, thumbnail } = useContext(SettingsContext);
  const [isSelected, setIsSelected] = useState(false);
  const r = useRef<HTMLDivElement>(null);
  const size = useComponentSize(r);
  return (
    <div
      className={styles.container}
      style={{
        borderColor: isDragging ? 'black' : 'gray',
        minWidth: `${thumbnail.width}px`,
        width: isSelected ? '100%' : '1%',
        height: isSelected ? 'auto' : `${thumbnail.height}px`,
      }}
      ref={x => {
        ((ref as unknown) as (f: HTMLDivElement | null) => void)(x);
        (r as { current: HTMLDivElement | null }).current = x;
      }}
    >
      <StyledFileToolbar urlPath={urlPath} onZoom={() => setIsSelected(!isSelected)} />
      <Thumbnail src={`${filePreviewApi}${urlPath}`} width={size.width} />
      <span className={styles.caption} title={name}>
        {name}
      </span>
    </div>
  );
});
