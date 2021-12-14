import re
import codecs


ELEMENTS_RAW_PATH = 'data/elements_raw.txt'
NODES_RAW_PATH = 'data/nodes_raw.txt'

ELEMENTS_PATH = 'data/triangleElements.csv'
NODES_PATH = 'data/nodes2D.csv'


# TRI_3 1 (internal 0) : part PART_1_1_1, nodes (internal 102 205 167)
EL_PATTERN = re.compile(r"internal\s(\d+).*\s(\d+)\s(\d+)\s(\d+)")
# node 190 (internal 189) : dim 2, pos 0 -0.0132555 0.0280437
NODE_PATTERN = re.compile(r"internal\s(\d+).*\s(-?\d+(\.\d+)?(e-\d+)?)\s(-?\d+(\.\d+)?(e-\d+)?)\s(-?\d+(\.\d+)?(e-\d+)?)")


with codecs.open(ELEMENTS_RAW_PATH, "r", "utf-8") as input_file:
    with codecs.open(ELEMENTS_PATH, "w", "utf-8") as output_file:
        output_file.write("Id;NodesIds;Thickness\n")
        for line in input_file:
            if line.startswith("#"):
                continue
            line = line.strip()
            result = re.search(EL_PATTERN, line)
            if result is not None:
                el_id = result.group(1)
                id1 = result.group(2)
                id2 = result.group(3)
                id3 = result.group(4)
                output_file.write(f"{el_id};{id1}|{id2}|{id3};0,001\n")


with codecs.open(NODES_RAW_PATH, "r", "utf-8") as input_file:
    with codecs.open(NODES_PATH, "w", "utf-8") as output_file:
        output_file.write("Id;X;Y;Z;Type;Force value;Force angle\n")
        for line in input_file:
            line = line.strip()
            if line.startswith("#"):
                continue
            if line.startswith("node"):
                result = re.search(NODE_PATTERN, line)
                if result is not None:
                    node = int(result.group(1))
                    x = str(float(result.group(2))).replace('.', ',')
                    y = str(float(result.group(5))).replace('.', ',')
                    z = str(float(result.group(8))).replace('.', ',')
                    output_file.write(f"{node};{x};{y};{z};None;null;null\n")