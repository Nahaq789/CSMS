import TaskCard from '../components/list-Items/TaskListItem';
import React, {FC, useState} from "react";

interface Task {
    id: string;
    name: string;
    content: string;
}

interface TaskProps {
    task: Task
}

const [ task, setTask ] = useState<TaskProps>([]);

const Home: React.FC<TaskProps> = ({ task }): React.JSX.Element => {
return(
    <div className={'home-container'}>
      <TaskCard task={ task } />
    </div>
)};

export  default Home;
