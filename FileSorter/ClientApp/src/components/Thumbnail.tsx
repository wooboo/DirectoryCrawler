import React from "react";
interface Props {
  src: string;
  alt?: string;
  width?: number|null;
  height?: number|null;
}
function Thumbnail({ src, width, height, alt }: Props) {
  const sizes = [width && `width=${width}`, height && `height=${height}`];

  const size = sizes.filter(o => o).join("&");
  return <img src={`${src}?${size}`} style={{ width, height }} alt={alt} />;
}

export default Thumbnail;
