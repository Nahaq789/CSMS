import React from "react";
import { list } from "postcss";
import { List } from "postcss/lib/list";
import TaskListItem from "@/components/list-Items/TaskListItem";
import "../../app/globals.css";

interface Task {
  taskId: string;
  taskName: string;
  contents: string;
}

interface TaskProps {
  task: Task[];
}
const TaskListContainer: React.FC<TaskProps> = ({
  task,
}): React.JSX.Element => {
  return (
    <div className={"task-list-container"}>
      <ul>
        {task.map((task) => (
          <li key={task.taskId}>
            <TaskListItem task={task} />
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TaskListContainer;
