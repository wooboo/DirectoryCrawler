import React from 'react';
import styled from 'styled-components';

const Image = styled.img`
  width: 100%;
  border-radius: 4px;
`;
interface Props {
  src: string;
  alt?: string;
  width?: number | null;
  height?: number | null;
}
function Thumbnail({ src, width, height, alt }: Props) {
  const sizes = [width && `width=${width}`, height && `height=${height}`];

  const size = sizes.filter(o => o).join('&');
  return <Image src={`${src}?${size}`} alt={alt} />;
}

export default Thumbnail;
