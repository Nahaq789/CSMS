"use client";

import { AxiosResponse } from "axios";
import axios from "../../api/apiConfig";
import { promises } from "dns";
import React from "react";
import useSWR from "swr";
import styles from "./task.module.css";
import { Box, LinearProgress } from "@mui/material";
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
} from "@mui/x-data-grid";
import { idID } from "@mui/material/locale";

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
  const fetcher = <T,>(url: string): Promise<T> =>
    axios.get(url).then((res: AxiosResponse<T>) => res.data);
  const { data, error } = useSWR<GridRowsProp<Task>>("/api/Task/", fetcher);
  const [rows, setRows] = React.useState(data);
  const [rowModesModel, setRowModesModel] = React.useState<GridRowModesModel>(
    {}
  );

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

  const handleDeleteClick = (id: GridRowId) => () => {
    setRows(rows?.filter((row) => row.taskId !== id));
  };

  const handleCancelClick = (id: GridRowId) => () => {
    setRowModesModel({
      ...rowModesModel,
      [id]: { mode: GridRowModes.View, ignoreModifications: true },
    });

    const editedRow = rows?.find((row) => row.taskId === id);
    setRows(rows?.filter((row) => row.taskId !== id));
  };

  const processRowUpdate = (newRow: GridRowModel) => {
    const UpdateRow: Task = {
      ...newRow,
      taskId: "",
      taskName: "",
      contents: "",
      customerId: "",
      contractId: "",
    };
    setRows(
      rows?.map((row) => (row.taskId === newRow.taskId ? UpdateRow : row))
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
    },
    {
      field: "contents",
      headerName: "Content",
      flex: 1,
    },
    {
      field: "customerId",
      headerName: "CustomerID",
      flex: 1,
    },
    {
      field: "contractId",
      headerName: "ContractID",
      flex: 1,
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
                  backgroundColor: blueGrey[200],
                  borderBottom: "none",
                  // color: "#b7bac1",
                  borderRadius: "10px",
                },
                "& .MuiDataGrid-virtualScroller": {
                  // backgroundColor: "red",
                },
                "& .MuiDataGrid-footerContainer": {
                  borderTop: "none",
                  backgroundColor: blueGrey[200],
                  // color: "#b7bac1",
                  borderRadius: "10px",
                },
                "& .MuiCheckbox-root": {
                  color: green,
                },
                "& .MuiDataGrid-toolbarContainer .MuiButton-text": {
                  color: "#b7bac1",
                },
              }}
            >
              <DataGrid
                rows={data || []}
                columns={columns}
                slots={{
                  toolbar: GridToolbar,
                }}
                getRowId={(row) => row.taskId}
                onRowClick={console.log}
                disableRowSelectionOnClick
              />
            </Box>
          </Box>
        </div>
      </div>
    </div>
  );
};

export default Task;
