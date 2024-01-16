"use client";

import { AxiosResponse } from "axios";
import axios from "../../api/apiConfig";
import { promises } from "dns";
import React, { useEffect, useState } from "react";
import useSWR from "swr";
import styles from "./task.module.css";
import { Box, LinearProgress, rgbToHex } from "@mui/material";
import { ContrastOutlined, Key } from "@mui/icons-material";
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
} from "@mui/x-data-grid";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/DeleteOutlined";
import SaveIcon from "@mui/icons-material/Save";
import CancelIcon from "@mui/icons-material/Close";
import { text } from "stream/consumers";

interface Task {
  TaskId: string;
  TaskName: string;
  Contents: string;
  Deadline: Date;
  CustomerId: string;
  ContractId: string;
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

  const handleClick = () => {
    const id = "550e8400-e29b-41d4-a716-446655440001";
    setRows((oldRows) => [...oldRows, { id, name: "", age: "", isNew: true }]);
    setRowModesModel((oldModel) => ({
      ...oldModel,
      [id]: { mode: GridRowModes.Edit, fieldToFocus: "name" },
    }));
  };
}

const Task: React.FC<TaskProps> = (): React.JSX.Element => {
  const fetcher = async <T,>(url: string): Promise<T> =>
    await axios.get(url).then((res: AxiosResponse<T>) => res.data);
  //const { data, error } = useSWR<GridRowsProp<Task>>("/api/Task/", fetcher);
  const [task, setTask] = useState<GridRowsProp<Task>>();
  const [rows, setRows] = React.useState(task);
  const [rowModesModel, setRowModesModel] = React.useState<GridRowModesModel>(
    {}
  );
  const [getResult, setGetResult] = React.useState<Task>();
  const [text, setText] = useState<string>("");

  useEffect(() => {
    axios.get("/api/Task/").then((res) => {
      setTask(res.data);
      setRows(task);
    });
  }, [task]);

  const handleRowEditStop: GridEventListener<"rowEditStop"> = (
    params,
    event
  ) => {
    if (params.reason === GridRowEditStopReasons.rowFocusOut) {
      event.defaultMuiPrevented = true;
    }
  };

  const handleEditClick = (id: GridRowId) => () => {
    setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.Edit } });
  };

  // const handleSaveClick = (params: Task) => () => {
  //   const updateRow = {
  //     Task: params,
  //   };
  //   console.log(params.TaskId);

  //   setRowModesModel({
  //     ...rowModesModel,
  //     [params.TaskId]: { mode: GridRowModes.View, ignoreModifications: true },
  //   });
  //   console.log(rowModesModel);
  // };
  const handleSaveClick = (id: GridRowId) => async () => {
    setRowModesModel({
      ...rowModesModel,
      [id]: { mode: GridRowModes.View, ignoreModifications: true },
    });
  };

  const handleCancelClick = (id: GridRowId) => () => {
    setRowModesModel({
      ...rowModesModel,
      [id]: { mode: GridRowModes.View, ignoreModifications: true },
    });

    const editedRow = rows?.find((row) => row.TaskId === id);
    setRows(rows?.filter((row) => row.TaskId !== id));
  };

  const handleDeleteClick = (id: GridRowId) => async () => {
    await axios
      .delete(`/api/Task/${id}`)
      .then((res: AxiosResponse<Task>) => {
        setRows(rows?.filter((row: Task) => row.TaskId != id));
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

  const processRowUpdate = (newRow: GridRowModel<Task>) => {
    const updateRow = {
      ...newRow,
      isNew: false,
    };
    setRows(
      rows?.map((row) => (row.TaskId === newRow.TaskId ? updateRow : row))
    );
    return updateRow;
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
      field: "customerId",
      headerName: "CustomerID",
      flex: 1,
      editable: true,
    },
    {
      field: "contractId",
      headerName: "ContractID",
      flex: 1,
      editable: true,
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
                rowModesModel={rowModesModel}
                onRowModesModelChange={handleRowModesModelChange}
                onRowEditStop={handleRowEditStop}
                slots={{
                  toolbar: GridToolbar,
                }}
                onRowClick={console.log}
                disableRowSelectionOnClick
                slotProps={{
                  toolbar: { setRows, setRowModesModel },
                }}
                processRowUpdate={(updateRow) => {
                  processRowUpdate(updateRow);
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
