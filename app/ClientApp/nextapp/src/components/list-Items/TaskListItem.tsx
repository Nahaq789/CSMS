import * as React from 'react';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import {FC} from "react";
import {Inter} from "next/font/google";


interface Task {
    id: string;
    name: string;
    content: string;
    }

interface TaskProps {
    task: Task
}

const bull: React.JSX.Element = (
    <Box
        component="span"
        sx={{ display: 'inline-block', mx: '2px', transform: 'scale(0.8)' }}
    >
        •
    </Box>
);

const TaskCard: FC<TaskProps> = ({ task }): React.JSX.Element => {
    return (
        <Card sx={{ minWidth: 275 }}>
            <CardContent>
                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                    {task.id}
                </Typography>
                <Typography variant="h5" component="div">
                    {task.name}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    {task.content}
                </Typography>
                <Typography variant="body2">
                    well meaning and kindly.
                    <br />
                    {'"a benevolent smile"'}
                </Typography>
            </CardContent>
            <CardActions>
                <Button size="small">Learn More</Button>
            </CardActions>
        </Card>
    );
}

export default TaskCard;