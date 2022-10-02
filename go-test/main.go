package main

import (
	"encoding/json"
	"fmt"
	"os"

	aep "github.com/boltframe/aftereffects-aep-parser"
)

func main() {
	arg := os.Args[1]

	project, err := aep.Open(arg)
	if err != nil {
		panic(err)
	}

	b, err := json.Marshal(project)
	if err != nil {
		fmt.Println(err)
		return
	}
	fmt.Println(string(b))
}
