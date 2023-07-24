import DataGrid, {
  Column, Editing, RequiredRule, Lookup, Form, Popup, Item, MasterDetail, Toolbar
} from 'devextreme-react/data-grid';
import CustomStore from 'devextreme/data/custom_store';
import * as api from './api';
import Tasks from './Tasks';

import * as enums from './enums'
import * as common from './common'



function Projects() {

  const projectsData = new CustomStore({
    key: 'id',
    load: () => api.getProjects(),
    insert: (values) => api.createProject(JSON.stringify(values)),
    update: (key, values) => api.updateProject(key, JSON.stringify(values)),
    remove: (key) => api.deleteProject(key)
  });

  const onRowValidating = (e) => {
    if(e.newData.start || e.newData.end) {
      var start = e.newData.start || e.oldData.start;
      var end = e.newData.end || e.oldData.end;

      if(start && end) {
        e.isValid = end > start;
        e.errorText = "Start date must be less or equal to End date ";
      }
    }
  }

  return (
    <div className="App">
      <h1>Freelance.ProjectManagement</h1>
      <DataGrid
        showBorders={true}
        dataSource={projectsData}
        onRowUpdating={common.dataGridOnRowUpdating}
        onRowValidating={onRowValidating}>

          <Toolbar>
              <Item location="before">
                <h2 className='header'>Projects:</h2>
              </Item>
              <Item name="addRowButton" />
          </Toolbar>

        <Editing
          mode="popup"
          allowUpdating={true}
          allowDeleting={true}
          allowAdding={true}>
          <Popup title="Project Info" showTitle={true} width={700} height={400} />
          <Form>
          
              <Item dataField="name" colSpan={2}/>
              <Item dataField="start" />
              <Item dataField="status" />
              <Item dataField="end" />
            
          </Form>
        </Editing>

        <Column dataField="name">
          <RequiredRule />
        </Column>
        <Column dataField="start" dataType="date"/>
        <Column dataField="end" dataType="date"/>
        <Column dataField="status">
          <Lookup
            dataSource={enums.projectStatus}
            valueExpr="id"
            displayExpr="name" />
        </Column>

        <MasterDetail enabled={true} component={Tasks} />

      </DataGrid>
    </div>
  );
}

export default Projects