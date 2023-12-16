import Card from './Card'
import React from "react";
import styles from './card.module.css';

interface Task {
    taskId: string;
    taskName: string;
    contents: string;
}

interface TaskProps {
    task: Task[];
}

const CardContainer: React.FC<TaskProps> = ({
                                                task,
                                            }): React.JSX.Element => {
    return (
        <ul className={styles.ul}>
            {task.map((task) => (
                <li key={task.taskId} className={styles.list}>
                    {/*<Card task={task}/>*/}
                </li>
            ))}
        </ul>
    )
}

export default CardContainer;