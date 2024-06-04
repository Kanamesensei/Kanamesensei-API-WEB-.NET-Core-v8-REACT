import { Button, Table } from "reactstrap";


const TablaLibros = ({ data, setEditar, mostrarModal, setMostrarModal, eliminarLibro }) => {

    const enviarDatos = (libro) => {
        setEditar(libro)
        setMostrarModal(!mostrarModal)
    }


    return (

        <Table striped responsive>
            <thead>
                <tr>
                    <th>LibroID</th>
                    <th>Nombre</th>
                    <th>Genero</th>
                    <th>Descripcion</th>
                    <th>Estatus</th>
                </tr>
            </thead>
            <tbody>
                {
                    (data.length < 1) ? (
                        <tr>
                            <td colSpan="4">Sin registros</td>
                        </tr>
                    ) : (
                        data.map((item) => (

                            <tr key={item.libroID}>
                                <td>{item.libroID}</td>
                                <td>{item.nombre}</td>
                                <td>{item.genero}</td>
                                <td>{item.descripcion}</td>
                                <td>{item.estatus}</td>
                                <td>
                                    <Button color="primary" size="sm" className="me-2"
                                        onClick={() => enviarDatos(item)}
                                    >Editar</Button>
                                    <div>
                                        &nbsp; 
                                    </div>
                                    <Button color="danger" size="sm"
                                        onClick={() => eliminarLibro(item.libroID)}
                                    >Eliminar</Button>
                                </td>
                            </tr>
                        ))
                    )
                }
            </tbody>
        </Table>
    )

}

export default TablaLibros;
