import Company from './Company';
import CompanyList from './CompanyList';
import Directory from './Directory';
import { Meta } from '../../Meta';

export default {
    "Company": Company,
    "Directory": Directory,
    "CompanyList": CompanyList
} as { [key: string]: React.FunctionComponent<Meta> }