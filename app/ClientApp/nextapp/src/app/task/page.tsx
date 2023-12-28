"use client";

import { AxiosResponse } from "axios";
import axios from "../../api/apiConfig";
import { promises } from "dns";
import React from "react";
import useSWR from "swr";
import styles from "./task.module.css";
import { Box } from "@mui/material";
import { ContrastOutlined } from "@mui/icons-material";
import { red } from "@mui/material/colors";

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
          <h2 className={styles.title}>Task</h2>
          <ul>
            <Box sx={{ padding: "20px", display: "flex" }}>
              <Box>
                <Box
                  sx={{
                    margin: "10px 0 20px 20px",
                    gap: "20px",
                  }}
                >
                  <span className={styles.column}>Name</span>
                </Box>
                {data &&
                  data.map((item) => (
                    <Box
                      key={item.taskId}
                      sx={{
                        padding: "10px 20px 20px 20px",
                        gap: "20px",
                        fontSize: "1.5em",
                      }}
                    >
                      <Box>{item.taskName}</Box>
                    </Box>
                  ))}
              </Box>
              <Box>
                <Box
                  sx={{
                    margin: "10px 0 20px 20px",
                    gap: "20px",
                  }}
                >
                  <span className={styles.column}>Contents</span>
                </Box>
                {data &&
                  data.map((item) => (
                    <Box
                      key={item.taskId}
                      sx={{
                        padding: "10px 20px 20px 20px",
                        display: "flex",
                        gap: "20px",
                        fontSize: "1.5em",
                      }}
                    >
                      <Box>{item.contents}</Box>
                    </Box>
                  ))}
              </Box>
            </Box>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Task;
