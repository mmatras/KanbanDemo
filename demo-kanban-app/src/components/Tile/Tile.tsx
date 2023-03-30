import { IIssueDto } from "../../dtos/project";
import "./Tile.css";

type TilePorps = {
  issue: IIssueDto;
};

function Tile({ issue }: TilePorps) {
  const isUrgent = issue.isUrgent ? "!" : "";

  return (
    <a className="list-group-item tile" href={`/issue/edit/${issue.id}`}>
      <div className="row title">
        <div className="col-md-8">
          <em>{issue.title}</em>
          <span className="isUrgent">{isUrgent}</span>
        </div>
        <div className="col-md-4 assigned-to">
          {issue.assignedPersonDisplayName}
        </div>
      </div>
      <div className="row">
        <div className="col-md-12">
          <span className="desc">Descriptoin:</span>
          {issue.notes}
        </div>
      </div>
    </a>
  );
}

export default Tile;
