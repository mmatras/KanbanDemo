import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ClassicComponent from "./components/ClassicComponent/ClassicComponent";
import IssueView from "./components/IssueView/IssueView";
import "bootstrap/dist/css/bootstrap.css";
import IssueEdit from "./components/IssueEdit/IssueEdit";

const router = createBrowserRouter([
  {
    path: "/",
    element: <IssueView message={"hello"} />,
  },
  {
    path: "/issue/edit/:id",
    element: <IssueEdit />,
  },
  {
    path: "/classic-component",
    element: <ClassicComponent message="this is classic component" />,
  },
  {
    path: "/issues-view",
    element: <App />,
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <div className="container">
      <main role="main" className="pb-3">
        <RouterProvider router={router} />
      </main>
    </div>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
