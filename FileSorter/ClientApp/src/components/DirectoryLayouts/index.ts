import CompanyList from './CompanyList_';
import Directory from './Directory';
import Temp from './Temp';
import { Meta } from '../../Meta';
import MonthFull from './MonthFull';

export default {
  Directory: Directory,
  _CompanyList: CompanyList,
  Temp: Temp,
  MonthFull: MonthFull,
} as { [key: string]: React.FunctionComponent<Meta> };
