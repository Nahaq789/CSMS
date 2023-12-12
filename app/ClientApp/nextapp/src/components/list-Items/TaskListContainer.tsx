import React from "react";
import {list} from "postcss";
import {List} from "postcss/lib/list";
import TaskListItem from "@/components/list-Items/TaskListItem";

interface Task {
    id: string;
    name: string;
    content: string;
}

interface TaskProps {
    task: Task[]
}
const TaskListContainer: React.FC<TaskProps> = ({ task }): React.JSX.Element => {
    return (
        <div className={'task-list-container'}>
            <ul>
                {
                    task.map((task) => 
                        <TaskListItem task={ task } />
                    )
                }
            </ul>
        </div>
    )
}

export default TaskListContainer;