import styles from './card.module.css'
import React, {FC} from "react";
import {Spa} from "@mui/icons-material";
import {MdSupervisedUserCircle} from "react-icons/md";

interface Task {
    taskId: string;
    taskName: string;
    contents: string;
}

interface TaskProps {
    task: Task;
}

const Card = ({}): React.JSX.Element => {
    return (
        // <div className={styles.container}>
        //     <div className={styles.text}>
        //         <span className={styles.title}>{task.taskName}</span>
        //         <span className={styles.number}>{task.taskId}</span>
        //         <span className={styles.detail}>{task.contents}</span>
        //         {/*<span className={styles.detail}>*/}
        //         {/*    <span className={{}}>*/}
        //         {/*        */}
        //         {/*    </span>*/}
        //         {/*</span>*/}
        //     </div>
        // </div>
        <div className={styles.container}>
            <MdSupervisedUserCircle size={24}/>
            <div className={styles.text}>
                <div className={styles.title}>Users</div>
                <div className={styles.number}>10.273</div>
                <div className={styles.detail}>
                    <span className={styles.positive}>12%</span>than previous week
                </div>
            </div>
        </div>
    )
}

export default Card;