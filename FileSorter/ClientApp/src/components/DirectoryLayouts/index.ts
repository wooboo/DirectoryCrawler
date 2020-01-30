import Company from './Company';
import CompanyList from './CompanyList';
import Directory from './Directory';
import Temp from './Temp';
import { Meta } from '../../Meta';
import MonthFull from './MonthFull';

export default {
    "Company": Company,
    "Directory": Directory,
    "CompanyList": CompanyList,
    "Temp": Temp,
    "MonthFull": MonthFull
} as { [key: string]: React.FunctionComponent<Meta> }