"use client";

import {Box, TextField} from "@mui/material";
import axios from "../../../api/apiConfig";
import React, {useEffect, useState} from "react";
import {AxiosResponse} from "axios";
import DateField from "../../../components/datePicker/datePicker";
import {LocalizationProvider} from "@mui/x-date-pickers/LocalizationProvider";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DatePicker} from "@mui/x-date-pickers/DatePicker";
import {DateTimePicker} from "@mui/x-date-pickers";
import {DemoContainer} from "@mui/x-date-pickers/internals/demo";

interface Task {
    taskId: string;
    taskName: string;
    contents: string;
    deadline: Date | null;
    customerId: string;
    contractId: string;
    taskStatusId: number;
}

const TextFieldStyle = {
    width: "100%",
    "& .MuiInputBase-input": {
        color: "#AAAAAA", // 入力文字の色
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
    const [task, setTask] = useState<Task>();

    const [taskName, setTaskName] = useState<string>("");
    const [taskId, setTaskId] = useState<string>("");
    const [content, setContent] = useState<string>("");
    const [deadline, setDeadline] = useState<Date | null>(null);

    useEffect((): void => {
        const result = async (): Promise<void> => {
            const currentUrl: string = window.location.href;
            const idIndex: number = currentUrl.indexOf("id=") + 3;
            const id: string = currentUrl.slice(idIndex);
            await axios
                .get(`/api/Task/${id}`)
                .then((res: AxiosResponse<any, any>): void => {
                    setTask(res.data);
                    setTaskName(res.data.taskName);
                    setTaskId(res.data.taskId);
                    setContent(res.data.contents);
                    setDeadline(res.data.deadline);
                    console.log(res.data);
                });
        };
        result();
    }, []);

    const handleDateChange = (date: Date | null): void => {
        if (date) {
            setDeadline(date);
            console.log(date);
        }
    }

    const handleUpdateAsync = async (): Promise<void> => {
        const UpdateTaskModel: Task = {
            taskId: taskId,
            contents: content,
            contractId: taskId,
            customerId: taskId,
            deadline: deadline,
            taskName: taskName,
            taskStatusId: 1,
        }

        await axios.put("api/Task", UpdateTaskModel)
    }

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
            >np
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
                                value={taskId}
                            />
                            <TextField
                                required
                                id="taskname"
                                label="Task Name"
                                defaultValue=""
                                variant="standard"
                                sx={TextFieldStyle}
                                onChange={(e) => setTaskName(e.target.value)}
                                value={taskName}
                            />
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
                                id="content"
                                label="Content"
                                defaultValue=""
                                variant="standard"
                                sx={TextFieldStyle}
                                onChange={(e) => setContent(e.target.value)}
                                value={content}
                            />
                            <TextField
                                required
                                id="taskname"
                                label="Task Name"
                                defaultValue=""
                                variant="standard"
                                sx={TextFieldStyle}
                                onChange={(e) => setTaskName(e.target.value)}
                                value={taskName}
                            />
                        </Box>
                        <Box
                            sx={{
                                gap: "20px",
                                paddingTop: "20px",
                                display: "flex",
                                width: "50%"
                            }}
                        >
                            <LocalizationProvider dateAdapter={AdapterDayjs}>
                                <DemoContainer components={['DatePicker']}>
                                    <DatePicker
                                        label="Small picker"
                                        slotProps={{textField: {size: 'small'}}}
                                        sx={TextFieldStyle}
                                        value={deadline}
                                        onChange={handleDateChange}
                                    />
                                </DemoContainer>
                            </LocalizationProvider>
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
