import React, { Component, FunctionComponent, ComponentClass } from 'react';

interface Props {
    layout: string;
}
interface State {
    module?: FunctionComponent | ComponentClass | string;
}

export default class LayoutLoader extends Component<Props & React.Attributes, State> {
    // after the initial render, wait for module to load
    async componentDidMount(): Promise<void> {
        const { layout } = this.props;
        const { default: module } = await import(`./DirectoryLayouts/${layout}`);
        this.setState({ module });
    }

    render(): JSX.Element | undefined {
        const { module } = this.state;

        if (!module) return <div>Loading module...</div>;
        if (module != null) {
            return React.createElement(module, this.props);
        }
    }
}
