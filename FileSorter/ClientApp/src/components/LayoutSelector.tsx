import React from "react";
import { LayoutLoader } from "./LayoutLoader";
import layouts from './DirectoryLayouts';
import { Meta } from "../Meta";

const LayoutSelector = (props: Meta) => {
  const { properties } = props;
  const layout = (properties && properties.layout) || 'Directory';
  const component = layouts[layout as string] ?? layouts['Directory'];
  return React.createElement(component, props);
};

export default LayoutSelector;
