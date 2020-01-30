import React, { ReactElement } from 'react'
import { Properties, Meta } from '../Meta'
import { DirectoryListing, DirectoryEntry } from './Basics'
import LayoutSelector from './LayoutSelector'
import FileDrag from './FileDrag'
import File from './File';

interface Props {
    files: { [name: string]: Properties };
    directories: { [name: string]: Meta };
}

export default function DirectoryEntries({ directories, files }: Props): ReactElement {
    return (
        <>
            {directories &&
                <DirectoryListing>
                    {Object.entries(directories).map(([k, v]) => (
                        <DirectoryEntry key={k}><LayoutSelector {...v} key={k} /></DirectoryEntry>
                    ))}
                </DirectoryListing>}
            {files &&
                <DirectoryListing direction='row'>
                    {Object.entries(files).map(([k, v]) => (
                        <FileDrag urlPath={v.urlPath} name={k} key={k}>
                            <DirectoryEntry key={k}>
                                <File name={k} urlPath={v.urlPath} properties={v} width={100} height={150} />
                            </DirectoryEntry>
                        </FileDrag>
                    ))}
                </DirectoryListing>}
        </>
    )
}
