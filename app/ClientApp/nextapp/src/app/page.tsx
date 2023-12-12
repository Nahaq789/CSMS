'use client'
import TaskCard from '../components/list-Items/TaskListItem';
import React, {FC, useEffect, useState} from "react";
import axios from '../api/apiConfig';
import TaskListContainer from "@/components/list-Items/TaskListContainer";
interface Task {
    id: string;
    name: string;
    content: string;
    customerId: string;
    contractId: string;
}

interface TaskProps {
    task: Task[]
}

const Home: React.FC<TaskProps> = (): React.JSX.Element => {
    const [ tasks, setTasks ] = useState<Task[]>([]);
    useEffect(() => {
        axios.get("/api/Task").then((result) => {
            console.log(result.data);
            setTasks(result.data)
        })
    })
    
return(
    <div className={'home-container'}>
      <TaskListContainer task={ tasks } />
    </div>
)};

export  default Home;
