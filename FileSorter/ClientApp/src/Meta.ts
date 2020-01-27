export interface Properties {
  [key: string]: any;
}

export interface Meta<Props = Properties> {
  name: string;
  urlPath: string;
  files: { [name: string]: Properties };
  directories: { [name: string]: Meta };
  properties: Props;
}
