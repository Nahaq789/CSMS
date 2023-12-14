import * as React from 'react';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import {FC} from "react";
import {Inter} from "next/font/google";
import "../../app/globals.css"


interface Task {
    taskId: string;
    taskName: string;
    contents: string;
    }

interface TaskProps {
    task: Task
}
const TaskCard: FC<TaskProps> = ({ task }): React.JSX.Element => {
    return (
        <Card className={"card-main-container"}>
            <CardContent className={"card-content"}>
                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                    {task.taskId}
                </Typography>
                <Typography variant="h5" component="div">
                    {task.taskName}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    
                </Typography>
                <Typography variant="body2">
                    {task.contents}
                </Typography>
            </CardContent>
            <CardActions>
                <Button size="small">Learn More</Button>
            </CardActions>
        </Card>
    );
}

export default TaskCard;