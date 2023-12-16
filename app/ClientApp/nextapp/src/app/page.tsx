"use client";
import TaskCard from "../components/list-Items/TaskListItem";
import React, {FC, useEffect, useState} from "react";
import axios from "../api/apiConfig";
import TaskListContainer from "@/components/list-Items/TaskListContainer";
import {Result} from "postcss";
import styles from '../components/dashboard.module.css'
import Cards from '../components/card/Card'
import CardContainer from "@/components/card/CardContainer";
import Rightbar from "@/components/rightbar/RightBar";
import Box from "@mui/material/Box";

interface Task {
    taskId: string;
    taskName: string;
    contents: string;
    customerId: string;
    contractId: string;
}

interface TaskProps {
    task: Task[];
}

interface Card {
    id: string
    name: string
    content: string
    any: undefined
}

interface CardProps {
    card: Card;
}

const Home: React.FC<TaskProps> = (): React.JSX.Element => {
    const [tasks, setTasks] = useState<Task[]>([]);
    useEffect(() => {
        axios.get("/api/Task/", {
            params: {
                limit: 5
            }
        }).then((result) => {
            setTasks(result.data);
            console.log(result.data);
        });
    }, []);
    return (
        <div className={styles.wrapper}>
            <div className={styles.main}>
                <div className={styles.cards}>
                    {/*<div className={styles.cards_box}>*/}
                    {/*    <CardContainer task={tasks}/>*/}

                    {/*</div>*/}
                    <Cards/>
                    <Cards/>
                    <Cards/>
                    <Cards/>
                </div>
                <Box sx={{
                    display: 'flex',
                }}>
                    <Cards/>
                    <Cards/>
                    <Cards/>
                    <Cards/>
                </Box>
            </div>
        </div>
    );
};

export default Home;
