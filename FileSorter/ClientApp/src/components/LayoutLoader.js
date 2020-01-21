import React, { Component } from "react";

export class LayoutLoader extends Component {
    constructor(props) {
        super(props);

        this.state = {
            module: null
        };
    }

    // after the initial render, wait for module to load
    async componentDidMount() {
        const { layout } = this.props;
        const { default: module } = await import(`./DirectoryLayouts/${layout}`);
        this.setState({ module });
    }

    render() {
        const { module } = this.state;

        if (!module) return <div>Loading module...</div>;
        if (module) {
            console.log(module);
            return React.createElement(module, this.props);
        }
    }
}