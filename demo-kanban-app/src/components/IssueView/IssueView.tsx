import React from "react";
import { useEffect, useState } from "react";
import { IIssueDto, IssueState } from "../../dtos/project";
import Tile from "../Tile/Tile";

type IssueViewProps = {
  message: string;
};

function IssueView({ message }: IssueViewProps) {
  let [issuesTodo, setIssuesTodo] = useState([] as IIssueDto[]);
  let [issuesDoing, setIssuesDoing] = useState([] as IIssueDto[]);
  let [issuesDone, setIssuesDone] = useState([] as IIssueDto[]);

  useEffect(() => {
    const fetchIssues = async () => {
      const result = await fetch("/api/issue", {
        method: "GET",
      });

      const data = (await result.json()) as IIssueDto[];
      setIssuesTodo(data.filter((el) => el.state == IssueState.Todo));
      setIssuesDoing(data.filter((el) => el.state == IssueState.Doing));
      setIssuesDone(data.filter((el) => el.state == IssueState.Done));
    };
    fetchIssues();
  }, []); //compoentnDidMount

  return (
    <div className="row">
      <div className="col-md-4 list-group">
        <h3 className="tile-header">Todo</h3>
        {issuesTodo.map((issue) => (
          <Tile issue={issue} />
        ))}
      </div>
      <div className="col-md-4 list-group">
        <h3 className="tile-header">Doing</h3>
        {issuesDoing.map((issue) => (
          <Tile issue={issue} />
        ))}
      </div>
      <div className="col-md-4 list-group">
        <h3 className="tile-header">Done</h3>
        {issuesDone.map((issue) => (
          <Tile issue={issue} />
        ))}
      </div>
    </div>
  );
}

export default IssueView;
