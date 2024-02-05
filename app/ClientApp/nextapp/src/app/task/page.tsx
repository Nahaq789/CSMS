"use client";

import { AxiosResponse } from "axios";
import axios from "../../api/apiConfig";
import React, { useEffect, useState } from "react";
import useSWR from "swr";
import styles from "./task.module.css";
import { Box, LinearProgress, rgbToHex } from "@mui/material";
import Link from "next/link";
import { blue, blueGrey, green, grey, red } from "@mui/material/colors";
import {
  DataGrid,
  GridToolbar,
  GridColDef,
  GridRowsProp,
  GridRowModesModel,
  GridEventListener,
  GridRowEditStopReasons,
  GridRowId,
  GridRowModes,
  GridRowModel,
  GridActionsCellItem,
  GridRowProps,
  GridRowParams,
  MuiEvent,
  GridRowEditStopParams,
  MuiBaseEvent,
  GridRowMode,
  GridValueGetterParams,
} from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/DeleteOutlined";
import SaveIcon from "@mui/icons-material/Save";
import CancelIcon from "@mui/icons-material/Close";
import { useRouter } from "next/navigation";
import { UpdateRounded } from "@mui/icons-material";

interface Task {
  taskId: string;
  taskName: string;
  contents: string;
  deadline: Date;
  customerId: string;
  contractId: string;
  taskStatusId: number;
}

interface TaskProps {
  task: Task[];
}

interface EdirTollbarProps {
  setRows: (newRows: (oldRows: GridRowsProp) => GridRowsProp) => void;
  setRowModesModel: (
    newModel: (oldModel: GridRowsProp) => GridRowsProp
  ) => void;
}

function EditToolbar(props: EdirTollbarProps) {
  const { setRows, setRowModesModel } = props;

  // const handleClick = () => {
  // const id = romdom;
  // setRows((oldRows) => [...oldRows, { id, name: '', age: '', isNew: true }]);
  // setRowModesModel((oldModel) => ({
  //   ...oldModel,
  //   [id]: { mode: GridRowModes.Edit, fieldToFocus: 'name' },
  // }));
}

const Task: React.FC<TaskProps> = (): React.JSX.Element => {
  const fetcher = async <T,>(url: string): Promise<T> =>
    await axios.get(url).then((res: AxiosResponse<T>) => res.data);
  const { data, error } = useSWR<GridRowsProp<Task>>("/api/Task/", fetcher);
  const [task, setTask] = useState<GridRowsProp<Task>>();
  //const [rows, setRows] = React.useState(data);
  const [rowModesModel, setRowModesModel] = React.useState<GridRowModesModel>(
    {}
  );
  const [getResult, setGetResult] = React.useState<Task>();
  const [text, setText] = useState<string>("");
  const router = useRouter();
  useEffect(() => {
    if (data) {
      //setRows(data);
      setTask(data);
      console.log(data);
    }
  }, [data]);

  const handleRowEditStop: GridEventListener<"rowEditStop"> = (
    params: GridRowEditStopParams,
    event: MuiEvent<MuiBaseEvent>
  ) => {
    if (params.reason === GridRowEditStopReasons.rowFocusOut) {
      event.defaultMuiPrevented = false;
    }
  };

  const handleEditClick = (id: GridRowId) => () => {
    //setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.Edit } });
    const editUrl = `/task/id=${id}`;
    router.push(editUrl, { query: { id: id } });
  };

  const handleSaveClick = (id: GridRowId) => () => {
    setRowModesModel({
      ...rowModesModel,
      [id]: { mode: GridRowModes.View, ignoreModifications: false },
    });
  };

  const handleTaskGetAll = () => {
    axios.get("/api/Task/").then((res) => {
      setTask(res.data);
      //setRows(res.data);
    });
  };

  const handleDeleteClick = (id: GridRowId) => async () => {
    await axios
      .delete(`/api/Task/${id}`)
      .then((res: AxiosResponse<Task>) => {
        // setRows(rows?.filter((row: Task) => row.TaskId != id));
        // setTask(rows);
        handleTaskGetAll();
      })
      .catch((error) => {
        if (error.response) {
          console.log(error.response.data);
          console.log(error.response.status);
          console.log(error.response.headers);
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log("Error", error.message);
        }
        console.log(error.config);
      });
  };

  const handleCancelClick = (id: GridRowId) => () => {
    setRowModesModel({
      ...rowModesModel,
      [id]: { mode: GridRowModes.View, ignoreModifications: true },
    });

    const editedRow = task?.find((row: Task) => row.taskId === id);
    setTask(task?.filter((row: Task) => row.taskId !== id));
  };

  const processRowUpdate = async (newRow: GridRowModel) => {
    const updatedRow = { ...newRow } as Task;
    setTask(task?.map((row) => (row.taskId === newRow.id ? updatedRow : row)));

    console.log(updatedRow);

    await axios
      .put("/api/Task/", updatedRow)
      .then((res: AxiosResponse<Task>) => {
        console.log(res.data);
      });

    return updatedRow;
  };

  const handleRowModesModelChange = (newRowModesModel: GridRowModesModel) => {
    setRowModesModel(newRowModesModel);
  };

  const columns: GridColDef[] = [
    { field: "taskId", headerName: "ID", flex: 1 },
    {
      field: "taskName",
      headerName: "Name",
      flex: 1,
      cellClassName: "name-column--cell",
      editable: true,
    },
    {
      field: "contents",
      headerName: "Content",
      flex: 1,
      editable: true,
    },
    {
      field: "deadline",
      headerName: "Dead Line",
      flex: 1,
      editable: true,
      type: "dateTime",
      valueGetter: (params: GridValueGetterParams) => {
        const deadlineString = params.row.deadline as string;
        const deadlineDate = new Date(deadlineString);
        return deadlineDate;
      },
    },
    {
      field: "actions",
      type: "actions",
      headerName: "Actions",
      width: 100,
      cellClassName: "actions",
      getActions: (params: GridRowParams<Task>) => {
        const isInEditMode =
          rowModesModel[params.id]?.mode === GridRowModes.Edit;

        if (isInEditMode) {
          return [
            <GridActionsCellItem
              icon={<SaveIcon />}
              label="Save"
              sx={{
                color: "primary.main",
              }}
              onClick={handleSaveClick(params.id)}
              key={params.id}
            />,
            <GridActionsCellItem
              icon={<CancelIcon />}
              label="Cancel"
              className="textPrimary"
              onClick={handleCancelClick(params.id)}
              color="inherit"
              key={params.id}
            />,
          ];
        }
        return [
          <GridActionsCellItem
            icon={<EditIcon />}
            label="Edit"
            className="textPrimary"
            onClick={handleEditClick(params.id)}
            color="inherit"
            key={params.id}
          />,
          <GridActionsCellItem
            icon={<DeleteIcon />}
            label="Delete"
            onClick={handleDeleteClick(params.id)}
            color="inherit"
            key={params.id}
          />,
        ];
      },
    },
  ];

  return (
    <div className={styles.wrapper}>
      <div className={styles.main}>
        <div className={styles.container}>
          <Box>
            <h2 className={styles.title}>Task</h2>
            <Box
              m="40px 0 0 0"
              height="70vh"
              sx={{
                "& .MuiDataGrid-root": {
                  border: "none",
                },
                "& .MuiDataGrid-cell": {
                  borderBottom: "none",
                  color: "#b7bac1",
                },
                "& .name-column--cell": {
                  color: green[400],
                },
                "& .MuiDataGrid-columnHeaders": {
                  backgroundColor: "rgba(230, 234, 236, 0.4)",
                  borderBottom: "none",
                  // color: "#b7bac1",
                  borderRadius: "10px",
                },
                "& .MuiDataGrid-virtualScroller": {
                  // backgroundColor: "red",
                },
                "& .MuiDataGrid-footerContainer": {
                  borderTop: "none",
                  backgroundColor: "rgba(230, 234, 236, 0.4)",
                  // color: "#b7bac1",
                  borderRadius: "10px",
                },
                "& .MuiCheckbox-root": {
                  color: green,
                },
                "& .MuiDataGrid-toolbarContainer .MuiButton-text": {
                  // color: "#b7bac1",
                  color: "#b7bac1",
                },
              }}
            >
              <DataGrid
                rows={task || []}
                getRowId={(row) => row.taskId}
                columns={columns}
                editMode="row"
                processRowUpdate={(updatedRow, originalRow) =>
                  processRowUpdate(updatedRow)
                }
                onProcessRowUpdateError={(error) => console.log(error)}
                rowModesModel={rowModesModel}
                onRowModesModelChange={handleRowModesModelChange}
                onRowEditStop={handleRowEditStop}
                slots={{
                  toolbar: GridToolbar,
                }}
                //onRowClick={console.log}
                disableRowSelectionOnClick
                slotProps={{
                  toolbar: { setTask, setRowModesModel },
                }}
              />
            </Box>
          </Box>
        </div>
      </div>
    </div>
  );
};

export default Task;
