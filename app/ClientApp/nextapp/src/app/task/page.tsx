"use client";

import { AxiosResponse } from "axios";
import axios from "../../api/apiConfig";
import { promises } from "dns";
import React from "react";
import useSWR from "swr";
import styles from "./task.module.css";
import { Box } from "@mui/material";
import { ContrastOutlined } from "@mui/icons-material";

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

const Task: React.FC<TaskProps> = (): React.JSX.Element => {
  const fetcher = <T,>(url: string): Promise<T> =>
    axios.get(url).then((res: AxiosResponse<T>) => res.data);
  const { data, error } = useSWR<Task[]>("/api/Task/", fetcher);
  return (
    <div className={styles.wrapper}>
      <div className={styles.main}>
        <div className={styles.container}>
          <div className={styles.text}>Task</div>
          <ul>
            <li>
              {data &&
                data.map((item) => (
                  <Box key={item.taskId}>
                    {item.taskName}
                    {item.contents}
                  </Box>
                ))}
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Task;
