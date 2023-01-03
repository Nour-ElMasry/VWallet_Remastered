import Header from "./Header";

const AdminPageLayout = (props) => {
    return <div>
        <Header log admin/>
        {props.children}
    </div>;
}

export default AdminPageLayout;