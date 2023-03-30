import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {
  IEditIssueDto,
  IPersonSelectDto,
  IssueState,
} from "../../dtos/project";

type KeyValuePair = { name: string; value: string };

function IssueEdit() {
  let { id } = useParams();

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    console.log(issue);
  };

  let [issue, setIssue] = useState({
    id: 0,
    title: "",
    state: IssueState.Todo,
    notes: "",
    isUrgent: false,
    deadline: new Date().toISOString().substring(0, 10),
    assignedToId: 0,
  } as IEditIssueDto);

  let [issueStates, setIsseStates] = useState([] as KeyValuePair[]);
  let [people, setPeople] = useState([] as IPersonSelectDto[]);

  useEffect(() => {
    var is = [];
    for (const key in Object.keys(IssueState)) {
      const statusName = IssueState[key];
      if (typeof statusName === "string")
        is.push({ name: statusName, value: key });
    }
    setIsseStates(is);

    const featchPeople = async () => {
      const result = await fetch("/api/person/personSelect", {
        method: "GET",
      });
      var peopleData = (await result.json()) as IPersonSelectDto[];
      setPeople(peopleData);
    };
    featchPeople();
  }, []);

  useEffect(() => {
    const fetchIssue = async () => {
      const result = await fetch(`/api/issue/${id}`, {
        method: "GET",
      });

      var issueData = (await result.json()) as IEditIssueDto;
      setIssue(issueData);
    };
    fetchIssue();
  }, [id]);

  const handleStateChange = (event: ChangeEvent<HTMLSelectElement>) => {
    setIssue({ ...issue, state: parseInt(event.target.value) });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="row">
        <div className="col-md-6">
          <div className="form-group">
            <label htmlFor="title">Title:</label>
            <input
              className="form-control"
              value={issue.title}
              onChange={(event: ChangeEvent<HTMLInputElement>) => {
                setIssue({ ...issue, title: event.target.value });
              }}
              name="title"
              type="text"
            />
          </div>
        </div>
        <div className="col-md-6">
          <div className="form-group">
            <label htmlFor="state">State:</label>
            <select
              className="form-control"
              value={issue.state}
              onChange={handleStateChange}
            >
              <option>Select State...</option>
              {issueStates.map((is) => (
                <option key={is.value} value={is.value}>
                  {is.name}
                </option>
              ))}
            </select>
          </div>
        </div>
      </div>
      <div className="form-group">
        <label htmlFor="notes">Notes:</label>
        <textarea
          className="form-control"
          name="notes"
          value={issue.notes}
          onChange={(event: ChangeEvent<HTMLTextAreaElement>) => {
            setIssue({ ...issue, notes: event.target.value });
          }}
        ></textarea>
      </div>
      <div className="checkbox">
        <input
          className="checkbox"
          name="isUrgent"
          type="checkbox"
          value={issue.isUrgent.toString()}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            setIssue({ ...issue, isUrgent: event.target.value === "true" });
          }}
        />
        <label htmlFor="isUrgent">IsUrgent</label>
      </div>
      <div className="form-group">
        <label htmlFor="deadline">Deadline:</label>
        <input
          className="form-control"
          name="deadline"
          type="date"
          value={issue.deadline.substring(0, 10)}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            setIssue({ ...issue, deadline: event.target.value });
          }}
        />
      </div>
      <div className="form-group">
        <label htmlFor="assignedTo">Assigned To:</label>
        <select
          className="form-control"
          value={issue.assignedToId}
          onChange={(event: ChangeEvent<HTMLSelectElement>) => {
            setIssue({ ...issue, assignedToId: parseInt(event.target.value) });
          }}
        >
          <option>Select State...</option>
          {people.map((p) => (
            <option key={p.id} value={p.id}>
              {p.displayName}
            </option>
          ))}
        </select>
      </div>
      <button type="submit">Save</button>
    </form>
  );
}

export default IssueEdit;
