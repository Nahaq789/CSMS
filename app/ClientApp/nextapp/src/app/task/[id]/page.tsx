"use client";

import { Box, TextField } from "@mui/material";
import axios from "axios";
import { useSearchParams } from "next/navigation";
import { useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";

interface Task {
  taskId: string;
  taskName: string;
  contents: string;
  deadline: Date;
  customerId: string;
  contractId: string;
  taskStatusId: number;
}
const TextFieldStyle = {
  "& .MuiInputBase-input": {
    color: "#000000", // 入力文字の色
  },
  "& label": {
    color: "#AAAAAA", // 通常時のラベル色
  },
  "& .MuiInput-underline:before": {
    borderBottomColor: "#AAAAAA", // 通常時のボーダー色
  },
  "& .MuiInput-underline:hover:not(.Mui-disabled):before": {
    borderBottomColor: "#DDDDDD", // ホバー時のボーダー色
  },
  "& .MuiOutlinedInput-root": {
    "& fieldset": {
      borderColor: "#CCCCCC", // 通常時のボーダー色(アウトライン)
    },
    "&:hover fieldset": {
      borderColor: "#DDDDDD", // ホバー時のボーダー色(アウトライン)
    },
  },
};
const TaskPage: React.FC = () => {
  const match = location.pathname;
  console.log(match);
  const param = useSearchParams();
  const id = param.get("id");
  // const router = useRouter();
  // const id = router;
  const [task, setTask] = useState<Task>();

  useEffect(() => {
    const result = async () => {
      await axios.get(`/api/Task/${id}`).then((res) => {
        setTask(res.data);
        console.log(res.data);
      });
    };
    result();
  }, []);
  return (
    <Box
      sx={{
        display: "flex",
        gap: "20px",
        marginTop: "20px",
      }}
    >
      <Box
        sx={{
          flex: 3,
          display: "flex",
          flexDirection: "column",
          gap: "20px",
        }}
      >
        <Box
          sx={{
            background: "rgba(50, 50, 50, 0.5)",
            gap: "20px",
            borderRadius: "10px",
            padding: "20px",
          }}
        >
          <Box>
            <Box
              sx={{
                display: "flex",
                fontWeight: "bold",
                textTransform: "capitalize",
                color: "#b7bac1",
              }}
            >
              Update Task
            </Box>
            <Box
              sx={{
                gap: "20px",
                paddingTop: "20px",
                display: "flex",
              }}
            >
              <TextField
                required
                id="taskid"
                label="TaskID"
                defaultValue=""
                variant="standard"
                sx={TextFieldStyle}
              />
              <TextField
                required
                id="taskname"
                label="Task Name"
                defaultValue=""
                variant="standard"
                sx={TextFieldStyle}
              />
            </Box>
          </Box>
        </Box>
      </Box>
    </Box>
  );
};

export default TaskPage;
function useRouteMatch() {
  throw new Error("Function not implemented.");
}
