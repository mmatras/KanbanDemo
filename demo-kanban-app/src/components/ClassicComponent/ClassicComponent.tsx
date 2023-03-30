import { Component } from "react";

type Props = {
  message: string;
};

type State = {
  count: number;
};

class ClassicComponent extends Component<Props, State> {
  state: State = {
    count: 0,
  };

  increment = (n: number) => {
    this.setState((s) => ({ count: s.count + n }));
  };

  componentDidMount() {}

  render() {
    return (
      <div onClick={() => this.increment(3)}>
        {this.state.count} {this.props.message}
      </div>
    );
  }
}

export default ClassicComponent;
