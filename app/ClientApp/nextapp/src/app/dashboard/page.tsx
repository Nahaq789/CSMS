"use client";
import TaskCard from "../../components/list-Items/TaskListItem";
import React, { FC, useEffect, useState } from "react";
import axios from "../../api/apiConfig";
import styles from "../../components/dashboard.module.css";
import Cards from "../../components/card/Card";
import LineChart from "@/components/chart/linechart/LineChart";
import Box from "@mui/material/Box";
import PieChart from "@/components/chart/piechart/PieChart";

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
  id: string;
  name: string;
  content: string;
  any: undefined;
}

interface CardProps {
  card: Card;
}

const Dashboard: React.FC<TaskProps> = (): React.JSX.Element => {
  const [tasks, setTasks] = useState<Task[]>([]);
  useEffect(() => {
    axios
      .get("/api/Task/", {
        params: {
          limit: 5,
        },
      })
      .then((result) => {
        setTasks(result.data);
        console.log(result.data);
      });
  }, []);
  return (
    <div className={styles.wrapper}>
      <div className={styles.main}>
        <div className={styles.cards}>
          <Cards />
          <Cards />
          <Cards />
          <Cards />
        </div>
        <Box className={styles.row_2}>
          <LineChart />
          <PieChart />
        </Box>
      </div>
    </div>
  );
};

export default Dashboard;
