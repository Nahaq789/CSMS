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

  // const handleClick = () => {
  // const id = rows.find;
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
  const [rows, setRows] = React.useState(data);
  const [rowModesModel, setRowModesModel] = React.useState<GridRowModesModel>(
    {}
  );
  const [getResult, setGetResult] = React.useState<Task>();
  const [text, setText] = useState<string>("");
  useEffect(() => {
    if (data) {
      setRows(data);
    }
  }, [data]);
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

  const handleSaveClick = (id: GridRowId) => () => {
    setRowModesModel({ ...rowModesModel, [id]: { mode: GridRowModes.View } });
  };

  const handleDeleteClick = (id: GridRowId) => async () => {
    await axios.get(`/api/Task/${id}`).then((res: AxiosResponse<Task>) => {
      // setGetResult(res.data);
      axios
        .delete("/api/Task/", {
          headers: {
            "Content-Type": "application/json",
          },
          data: res.data,
        })
        .then((res) => {
          // setGetResult(res.data);
          setRows(rows?.filter((row) => row.TaskId !== id));
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

  const processRowUpdate = (newRow: GridRowModel) => {
    const UpdateRow: Task = {
      ...newRow,
      TaskId: "",
      TaskName: "",
      Contents: "",
      Deadline: new Date(),
      CustomerId: "",
      ContractId: "",
    };

    const payload: Task = {
      TaskId: UpdateRow.TaskId,
      TaskName: UpdateRow.TaskName,
      Contents: UpdateRow.Contents,
      Deadline: UpdateRow.Deadline,
      CustomerId: UpdateRow.CustomerId,
      ContractId: UpdateRow.ContractId,
    };
    setRows(
      rows?.map((row) => (row.TaskId === newRow.taskId ? UpdateRow : row))
    );
    return UpdateRow;
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
      getActions: ({ id }) => {
        const isInEditMode = rowModesModel[id]?.mode === GridRowModes.Edit;

        if (isInEditMode) {
          return [
            <GridActionsCellItem
              icon={<SaveIcon />}
              label="Save"
              sx={{
                color: "primary.main",
              }}
              onClick={handleSaveClick(id)}
              key={id}
            />,
            <GridActionsCellItem
              icon={<CancelIcon />}
              label="Cancel"
              className="textPrimary"
              onClick={handleCancelClick(id)}
              color="inherit"
              key={id}
            />,
          ];
        }
        return [
          <GridActionsCellItem
            icon={<EditIcon />}
            label="Edit"
            className="textPrimary"
            onClick={handleEditClick(id)}
            color="inherit"
            key={id}
          />,
          <GridActionsCellItem
            icon={<DeleteIcon />}
            label="Delete"
            onClick={handleDeleteClick(id)}
            color="inherit"
            key={id}
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
                rows={rows || []}
                columns={columns}
                editMode="row"
                rowModesModel={rowModesModel}
                onRowModesModelChange={handleRowModesModelChange}
                onRowEditStop={processRowUpdate}
                slots={{
                  toolbar: GridToolbar,
                }}
                getRowId={(row) => row.taskId}
                onRowClick={console.log}
                disableRowSelectionOnClick
                slotProps={{
                  toolbar: { setRows, setRowModesModel },
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
