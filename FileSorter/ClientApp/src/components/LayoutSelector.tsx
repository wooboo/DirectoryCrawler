import React from "react";
import { LayoutLoader } from "./LayoutLoader";
import layouts from './DirectoryLayouts';

const LayoutSelector = (props: {
  name: string;
  relativePath: string;
  files: any;
  directories: any;
  properties: any;
}) => {
  const { properties } = props;
  const layout = (properties && properties.layout) || 'Directory';
  console.log(layout)
  const component = layouts[layout];
  return React.createElement(component, props);
};

export default LayoutSelector;
