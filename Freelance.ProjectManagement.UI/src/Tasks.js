import DataGrid, {
    Column, Editing, RequiredRule, Lookup, Form, Popup, Item, Toolbar
  } from 'devextreme-react/data-grid';
import CustomStore from 'devextreme/data/custom_store';
import 'devextreme-react/text-area';
import * as api from './api'
import * as enums from './enums'
import * as common from './common'

function Tasks(props) {

    const projectId = props.data.key;
    
    const tasksData = new CustomStore({
        key: 'id',
        load: () => api.getTasksByProjectId(projectId),
        insert: (values) => api.createTask(projectId, JSON.stringify(values)),
        update: (key, values) => api.updateTask(key, JSON.stringify(values)),
        remove: (key) => api.deleteTask(key)
    });
  
    return (
      <div className='Tasks'>
        <DataGrid
        showBorders={true}
        dataSource={tasksData}
        onRowUpdating={common.dataGridOnRowUpdating}>
            <Toolbar>
                <Item location="before">
                    <h3 className='header'>Project Tasks:</h3>
                </Item>
                <Item name="addRowButton" />
            </Toolbar>
            <Editing
                mode="popup"
                allowUpdating={true}
                allowDeleting={true}
                allowAdding={true}>
                <Popup title="Task Info" showTitle={true} width={700} height={400} />
                <Form>
                    <Item dataField="name" colSpan={2}/>
                    <Item dataField="description" colSpan={2} editorType="dxTextArea" />
                    <Item dataField="status" />
                    <Item dataField="priority" editorType="dxNumberBox" editorOptions={ {min: 1, showSpinButtons: true, format: '#' } } />
                </Form>
            </Editing>

            <Column dataField="name">
                <RequiredRule />
            </Column>
            <Column dataField="description"/>
            <Column dataField="status">
                <Lookup
                    dataSource={enums.taskStatus}
                    valueExpr="id"
                    displayExpr="name" />
            </Column>
            <Column dataField="priority">
                <RequiredRule />
            </Column>
            
      </DataGrid>
      </div>
    );
  }
  
  export default Tasks;